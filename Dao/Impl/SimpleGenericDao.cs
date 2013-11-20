using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Dao.Impl {
    public class SimpleGenericDao<TEntity> where TEntity : class{
        public void Save(TEntity entity) {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction()) {
                session.Save(entity);
                transaction.Commit();
            }
        }

        public TEntity Get(long id) {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<TEntity>(id);
        }

        public IList<TEntity> GetaAll() {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var criteria = session.CreateCriteria<TEntity>();
                return criteria.List<TEntity>();
            }
        }

        public void Update(TEntity person) {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction()) {
                session.Update(person);
                transaction.Commit();
            }
        }

        public void Delete(TEntity person) {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction()) {
                session.Delete(person);
                transaction.Commit();
            }
        }

        public long RowCount() {
            using (ISession session = NHibernateHelper.OpenSession()) {
                return session.QueryOver<TEntity>().RowCountInt64();
            }
        }
    }// class
}
