﻿using System;
using System.Web.Mvc;
using Ninject;
using AdministrationPortal.ViewModels.Rules;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using System.Linq;
using NLog;

namespace AdministrationPortal.Controllers
{
    public class RulesController : Controller
    {
        [Inject]
        public Repository<Rule> RuleRepository { get; set; }
        [Inject]
        public Repository<RuleCategory> RuleCategoryRepository { get; set; }
        [Inject]
        public Repository<AlertType> AlertTypeRepository { get; set; }
        [Inject]
        public Repository<MessageType> MessageTypeRepository { get; set; }
        [Inject]
        public Repository<SupportCategory> SupportCategoryRepository { get; set; }
        [Inject]
        public Repository<Engine> EngineRepository { get; set; }
        [Inject]
        public Repository<DefaultNote> DefaultNoteRepository { get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET: Rules
        public ActionResult Index()
        {
            var rules = RuleRepository.Get();
            return View(rules);
        }

        // GET: Rules/Details/5
        public ActionResult Details(int id)
        {
            var rule = RuleRepository.GetById(id);
            if (rule == null)
                throw new ArgumentException("No Rule found with id " + id);

            return View(rule);
        }

        // GET: Rules/Create
        public ActionResult Create()
        {
            //intialize the dropdowns
            var viewModel = new RuleCreateViewModel
            {
                RuleOptions = new RuleOptionsViewModel()
                {
                    AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name"),
                    MessageTypes = new SelectList(MessageTypeRepository.Get(), "Name", "Name"),
                    RuleCategories = new SelectList(RuleCategoryRepository.Get(), "Id", "Name"),
                    SupportCategories = new SelectList(SupportCategoryRepository.Get(), "Id", "Name"),
                    EngineList = new SelectList(EngineRepository.Get(), "Name", "Name"),
                    DefaultNotes = new SelectList(DefaultNoteRepository.Get(), "Id", "Text")
                }
            };
            
            return View(viewModel);
        }

        
        
        // POST: Rules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RuleCreateViewModel ruleViewModel)
        {   
            //build rule
            var ruleToCreate = ruleViewModel.BuildRule(RuleCategoryRepository.Get());

            //insert new notes
            foreach (string newNote in ruleViewModel.NewDefaultNotes)
            {
                if (newNote.Trim() != "")
                {
                    ruleToCreate.DefaultNotes.Add(new DefaultNote { Text = newNote });
                }
            }
            
            //attatch existing notes
            foreach(int noteId in ruleViewModel.SelectedNoteIds)
            {
                if(noteId != 0)
                {
                    ruleToCreate.DefaultNotes.Add(DefaultNoteRepository.GetById(noteId));
                }
            }

            RuleRepository.Insert(ruleToCreate);
            RuleRepository.Save();

            return RedirectToAction("Index");
        }

        // GET: Rules/Edit/5
        public ActionResult Edit(int id)
        {
            var rule = RuleRepository.GetById(id);
            if (rule == null)
                throw new ArgumentException("No Rule found with id " + id);

            var viewModel = new RuleEditViewModel
            {
                RuleOptions = new RuleOptionsViewModel
                {
                    AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name"),
                    MessageTypes = new SelectList(MessageTypeRepository.Get(), "Name", "Name"),
                    RuleCategories = new SelectList(RuleCategoryRepository.Get(), "Id", "Name"),
                    SupportCategories = new SelectList(SupportCategoryRepository.Get(), "Id", "Name"),
                    EngineList = new SelectList(EngineRepository.Get(), "Name", "Name"),
                    DefaultNotes = new SelectList(DefaultNoteRepository.Get(), "Id", "Text")
                },
                RuleCreator = rule.RuleCreator,
                Description = rule.Description,
                Expression = rule.Expression,
                Name = rule.Name,
                MessageTypeName = rule.MessageTypeName,
                Engine = rule.Engine,
                Origin = rule.Origin,
                Server = rule.Server,
                DefaultSeverity = rule.DefaultSeverity,
                Id = id,
                DefaultNotes = rule.DefaultNotes.ToList()
            };

            return View(viewModel);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RuleEditViewModel ruleViewModel)
        {
            
            if (ModelState.IsValid)
            {
                var rule = RuleRepository.GetById(ruleViewModel.Id);
                ruleViewModel.Map(rule);

                if(ruleViewModel.DefaultNotes != null)
                {
                    foreach (DefaultNote note in ruleViewModel.DefaultNotes)
                    {

                        if (note.Text == null)
                            rule.DefaultNotes.Remove(DefaultNoteRepository.GetById(note.Id));
                        else
                            DefaultNoteRepository.GetById(note.Id).Text = note.Text;
                    }
                }
                
                

                //insert new notes
                foreach (string newNote in ruleViewModel.NewDefualtNotes)
                {
                    if (newNote != "")
                    {
                        rule.DefaultNotes.Add(new DefaultNote { Text = newNote });
                    }
                }

                //attatch existing notes
                foreach (int noteId in ruleViewModel.SelectedNoteIds)
                {
                    if (noteId != 0)
                    {
                        rule.DefaultNotes.Add(DefaultNoteRepository.GetById(noteId));
                    }
                }

                RuleRepository.Update(rule);
                RuleRepository.Save();

                return RedirectToAction("Index");
            }

            ruleViewModel.RuleOptions = new RuleOptionsViewModel
            {
                AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name"),
                MessageTypes = new SelectList(MessageTypeRepository.Get(), "Name", "Name"),
                RuleCategories = new SelectList(RuleCategoryRepository.Get(), "Id", "Name"),
                SupportCategories = new SelectList(SupportCategoryRepository.Get(), "Id", "Name"),
                EngineList = new SelectList(EngineRepository.Get(), "Name", "Name"),
                DefaultNotes = new SelectList(DefaultNoteRepository.Get(), "Id", "Text")
            };

            return View(ruleViewModel);
        }

        // GET: Rules/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var rule = RuleRepository.GetById(id);
            if (rule == null)
                throw new ArgumentException("No Rule found with id " + id);
            return View(rule);
        }

        // POST: Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rule = RuleRepository.GetById(id);
            if (rule == null)
                throw new ArgumentException("No Rule found with id " + id);

            foreach (DefaultNote note in rule.DefaultNotes)
            {
                note.Rules.Remove(rule);
            }

            RuleRepository.Delete(rule);
            RuleRepository.Save();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            Logger.Error(filterContext.Exception);

            filterContext.ExceptionHandled = true;

            // Redirect on error:
            filterContext.Result = RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RuleRepository.Dispose();
                RuleCategoryRepository.Dispose();
                AlertTypeRepository.Dispose();
                MessageTypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
