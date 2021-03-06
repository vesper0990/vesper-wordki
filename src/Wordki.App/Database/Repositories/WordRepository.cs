﻿using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Collections.Generic;
using Wordki.Models;
using WordkiModel;
using System.Threading.Tasks;
using Wordki.Database.Repositories;

namespace Wordki.Database
{
    public class WordRepository : IWordRepository
    {
        public void Delete(IWord word)
        {
            if (!CheckObject(word))
            {
                return;
            }
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(word);
                transaction.Commit();
            }
        }

        public Task DeleteAsync(IWord word)
        {
            return Task.Run(() => Delete(word));
        }

        public IWord Get(long id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<IWord>(id);
            }
        }

        public Task<IWord> GetAsync(long id)
        {
            return Task.Run(() => Get(id));
        }

        public IEnumerable<IWord> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<IWord>().ToList();
            }
        }

        public Task<IEnumerable<IWord>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public long RowCount()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<IWord>().RowCountInt64();
            }
        }

        public Task<long> RowCountAsync()
        {
            return Task.Run(() => RowCount());
        }

        public void Save(IWord word)
        {
            if (!CheckObject(word))
            {
                return;
            }
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(word);
                transaction.Commit();
            }
        }

        public async Task SaveAsync(IWord word)
        {
            if (!CheckObject(word))
            {
                return;
            }
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                await session.SaveOrUpdateAsync(word);
                transaction.Commit();
            }
        }

        public void Update(IWord word)
        {
            if (!CheckObject(word))
            {
                return;
            }
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(word);
                transaction.Commit();
            }
        }

        public async Task UpdateAsync(IWord word)
        {
            if (!CheckObject(word))
            {
                return;
            }
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                await session.UpdateAsync(word);
                transaction.Commit();
            }
        }

        private bool CheckObject(IWord word)
        {
            return word != null
                && word.Id > 0
                && word.Group != FakeGroup.Group;
        }

        //TODO test it
        public void Save(IEnumerable<IWord> items)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (IWord item in items)
                {
                    if (!CheckObject(item))
                    {
                        continue;
                    }
                    session.SaveOrUpdate(item);
                }
                transaction.Commit();
            }
        }

        public void Update(IEnumerable<IWord> items)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (IWord item in items)
                {
                    if (!CheckObject(item))
                    {
                        continue;
                    }
                    session.Update(item);
                }
                transaction.Commit();
            }
        }

        public async Task SaveAsync(IEnumerable<IWord> items)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (IWord item in items)
                {
                    if (!CheckObject(item))
                    {
                        continue;
                    }
                    await session.SaveOrUpdateAsync(item);
                }
                transaction.Commit();
            }
        }

        public async Task UpdateAsync(IEnumerable<IWord> items)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (IWord item in items)
                {
                    if (!CheckObject(item))
                    {
                        continue;
                    }
                    await session.UpdateAsync(item);
                }
                transaction.Commit();
            }
        }
    }
}
