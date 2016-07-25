using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using WatchdogDatabaseAccessLayer.Models;


namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFDefaultNoteRepositroy : Repository<DefaultNote>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFDefaultNoteRepositroy(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override void Dispose()
        {

        }

        public override IEnumerable<DefaultNote> Get()
        {
            return _container.DefaultNotes.ToList();
        }

        public override DefaultNote GetById(int id)
        {
            return _container.DefaultNotes.Find(id);
        }

        public override DefaultNote GetByName(string name)
        {
            return _container.DefaultNotes.First(note => note.Text == name);
        }

        public override void Insert(DefaultNote model)
        {
            _container.DefaultNotes.Add(model);
        }

        public override void Delete(DefaultNote model)
        {
            _container.DefaultNotes.Remove(model);
        }

        public override void Update(DefaultNote model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}