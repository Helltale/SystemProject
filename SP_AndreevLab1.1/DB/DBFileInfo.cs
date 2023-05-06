using System;
using FluentNHibernate.Mapping;

namespace DB
{
    public class DBFileInfo
    {
        public virtual long Id { get; set; }
        public virtual string path { get; set; }
        public virtual string size { get; set; }
        public virtual string date_ { get; set; }
    }

    public class ElementHibernateMap : ClassMap<DBFileInfo>
    {
        public ElementHibernateMap()
        {
            Id(obj => obj.Id);
            Map(obj => obj.path);
            Map(obj => obj.size);
            Map(obj => obj.date_);
        }
    }

}
