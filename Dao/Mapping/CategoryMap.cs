using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Domain;

namespace Dao.Mapping {
    public class CategoryMap  : ClassMapping<Category>{
        public CategoryMap() {
            Id(x => x.Id, m => { m.Column("CategoryId"); m.Generator(Generators.Native); });
            Property(x => x.CategoryName);
            Property(x => x.ImageName);
            Property(x => x.ImageExtension);
            Property(x => x.Enable);
            Property(x => x.Priority);
            Bag<CategoryItem>(x => x.Items, 
                              collectionMap => {
                                  collectionMap.Cascade(Cascade.All);
                                  collectionMap.Key(x => x.Column("CategoryId")); 
                                  collectionMap.Lazy(CollectionLazy.NoLazy);
                              },
                              map => map.OneToMany());
        }
    }// class
}
