public class DatabaseEngine {
    private static ISessionFactory CreateSessionFactory()
    {
        var connectionString = "DATA SOURCE=orcl;PERSIST SECURITY INFO=True;USER ID=scott;Password=tiger";

        var cfg = OracleClientConfiguration.Oracle11
            .ConnectionString(c => c.Is(connectionString));

        return Fluently.Configure()
            .Database(cfg)
            // .Mappings(m => m.FluentMappings.AddFromAssemblyOf<IEntity>().ExportTo(@".\"))
            .ExposeConfiguration(BuildSchema)
            .BuildSessionFactory();
    }

    private static void BuildSchema(NHibernate.Cfg.Configuration config)
    {
        // this NHibernate tool takes a configuration (with mapping info in)
        // and exports a database schema from it
        new SchemaExport(config)
          .Create(false, true);
    }
}

var dbEngine = new DatabaseEngine();
dbEngine.CreateSessionFactory();

