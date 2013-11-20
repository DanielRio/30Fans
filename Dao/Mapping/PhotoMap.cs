using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Domain;

namespace Dao.Mapping {
    public class PhotoMap : ClassMapping<Photo>{
        public PhotoMap() {
            Id(x => x.Id, map => { map.Column("PhotoId"); map.Generator(Generators.Identity); });
            Property(x => x.PhotoName);
            Property(x => x.Extension);
            Property(x => x.Text);
            ManyToOne(x => x.Product, map => {
                map.Class(typeof(Product));
                map.Column("ProductId");
            });
        }
    } // class
}
