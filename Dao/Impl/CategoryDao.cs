using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Dao.Impl {
    public class CategoryDao : SimpleGenericDao<Category>{
        public Category GetByName(string categoryName) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var criteria = session.CreateCriteria<Category>();
                criteria.Add(Expression.Eq("CategoryName",categoryName));
                var category = criteria.UniqueResult<Category>();
                return category != null ? category : new NullCategory();
            }
        }
    } //class
}
