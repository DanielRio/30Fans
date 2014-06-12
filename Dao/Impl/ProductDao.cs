using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Dao.Impl {
    public class ProductDao : SimpleGenericDao<Product>{
        public Product Get(long id) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var criteria = session.CreateCriteria<Product>();
                criteria.SetFetchMode("CategoryItem", FetchMode.Eager);
                criteria.SetFetchMode("CategoryItem.Category", FetchMode.Eager);
                criteria.Add(Expression.Eq("Id", id));
                return criteria.UniqueResult<Product>();
            }
        }


        public IList<Product> GetByPartName(string partName)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<Product>().Fetch(a => a.CategoryItem).Eager().Fetch(a => a.CategoryItem.Category).Eager().WhereRestrictionOn(p => p.ProductName).IsLike(partName, MatchMode.Anywhere).List(); 
   
            }
        }

        public IList<Product> GetByCategoryItemId(long categoryItemId) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var criteria = session.CreateCriteria<Product>();
                criteria.CreateAlias("CategoryItem", "categoryItem");
                criteria.SetFetchMode("categoryItem.Category", FetchMode.Eager);
                criteria.SetFetchMode("CategoryItem", FetchMode.Eager);
                criteria.SetFetchMode("CategoryItem.Category", FetchMode.Eager);
                criteria.Add(Expression.Eq("categoryItem.Id", categoryItemId));

                var productList = criteria.List<Product>();
                if (productList == null || productList.Count == 0)
                    throw new ProductNotFoundException("Product not found to this category item.");

                return productList;
            }                
        }
    }// class
}
