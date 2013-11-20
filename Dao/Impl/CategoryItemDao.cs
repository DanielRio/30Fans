using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Dao.Impl {
    public class CategoryItemDao : SimpleGenericDao<CategoryItem>{
        public IList<CategoryItem> GetByCategoryId(long categoryId) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var criteria = session.CreateCriteria<CategoryItem>();
                criteria.CreateAlias("Category", "category");
                criteria.Add(Expression.Eq("category.Id",categoryId));
                return criteria.List<CategoryItem>();
            }
        }

        public new CategoryItem Get(long id) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var criteria = session.CreateCriteria<CategoryItem>();
                criteria.SetFetchMode("Category", FetchMode.Eager);
                criteria.Add(Expression.Eq("Id", id));
                return criteria.UniqueResult<CategoryItem>();
            }
        }
    } //class
}
