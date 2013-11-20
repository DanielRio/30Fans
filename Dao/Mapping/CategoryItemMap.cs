using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Domain;

namespace Dao.Mapping {
    public class CategoryItemMap : ClassMapping<CategoryItem> {
        public CategoryItemMap() {
            Lazy(false);
            Id(x => x.Id, m => { m.Column("CategoryItemId"); m.Generator(Generators.Native); });
            Property(x => x.ItemName);
            Property(x => x.ImageName);
            Property(x => x.ImageExtension);
            Property(x => x.Enable);
            Property(x => x.Priority);
            ManyToOne<Category>(x => x.Category, 
                                m=> {
                                    m.Class(typeof(Category));
                                    m.Column("CategoryId");
                                });
        }
    } // class
}
