using System.Web.Mvc;
using Ninject;
using AdministrationPortal.ViewModels.Rules;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

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
            {
                return HttpNotFound("No Rule found with id " + id);
            }
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
            if (ruleViewModel.DefaultNoteText != null && ruleViewModel.DefaultNoteText != "")
            {
                DefaultNoteRepository.Insert(new DefaultNote{ Text = ruleViewModel.DefaultNoteText });
                DefaultNoteRepository.Save();
                ruleViewModel.DefaultNoteId = DefaultNoteRepository.GetByName(ruleViewModel.DefaultNoteText).Id;
            }

            
            var ruleToCreate = ruleViewModel.BuildRule(RuleCategoryRepository.Get());
            RuleRepository.Insert(ruleToCreate);
            RuleRepository.Save();

            return RedirectToAction("Index");
        }

        // GET: Rules/Edit/5
        public ActionResult Edit(int id)
        {
            var rule = RuleRepository.GetById(id);
            if (rule == null)
            {
                return HttpNotFound("No Rule found with Id " + id);
            }

            RuleEditViewModel viewModel;

            if(rule.DefaultNote != null)
            {
                viewModel = new RuleEditViewModel
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
                    DefaultNoteText = rule.DefaultNote.Text,
                    DefualtNoteTextEdited = rule.DefaultNote.Text
                };
            }
            else
            {
                viewModel = new RuleEditViewModel
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
                    DefaultNoteText = "No Note Added"
                };
            }


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
                EngineList = new SelectList(EngineRepository.Get(), "Name", "Name")
            };

            return View(ruleViewModel);
        }

        // GET: Rules/Delete/5
        public ActionResult Delete(int id)
        {
            var rule = RuleRepository.GetById(id);
            if (rule == null)
            {
                return HttpNotFound("No Rule found with Id " + id);
            }
            return View(rule);
        }

        // POST: Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rule = RuleRepository.GetById(id);
            if (rule == null)
            {
                return HttpNotFound("No Rule found with Id " + id);
            }

            RuleRepository.Delete(rule);
            RuleRepository.Save();
            return RedirectToAction("Index");
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
