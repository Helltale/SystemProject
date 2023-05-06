using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Reflection;

namespace DB
{
    internal class DBSession
    {
        public static ISession OpenSession()
        {
            string connectDB = "Data Source = HELLTALE\\SQLEXPRESS; Initial Catalog=SPDB; User Id=hell1; Password=1234";
            var config = new Configuration();
            config.DataBaseIntegration(d =>
            {
                d.ConnectionString = connectDB;
                d.Dialect<MsSql2012Dialect>();
                d.Driver<SqlClientDriver>();
            });

            config.AddAssembly(Assembly.GetExecutingAssembly());
            var sessionFactory = config.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }

        public static void CloseSession(ISession session)
        {
            session.Disconnect();
            session.Close();
        }
    }
}
