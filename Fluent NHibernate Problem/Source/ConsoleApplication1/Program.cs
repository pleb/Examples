using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ConsoleApplication1
{
    class Program
    {
        private const string ConnectionString = "Host=mysql1.local.test.server;User Id=Aieg;Password=Aieg;Database=AiegTest";

        static void Main()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DROP DATABASE AiegTest;";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE DATABASE AiegTest";
                    command.ExecuteNonQuery();
                }
            }

            var sessionFactory = CreateSessionFactory();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand("SHOW TABLES;", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0));
                        }
                    }
                }
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(ConnectionString))
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<Program>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Create(false, true);
        }
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id)
                .GeneratedBy.GuidComb();

            HasManyToMany(x => x.Retailers)
                .Access.BackingField()
                .Cascade.SaveUpdate()
                .AsSet();

            HasManyToMany(x => x.Regions)
                .Access.BackingField()
                .Cascade.SaveUpdate()
                .AsSet();
        }
    }

    public class RegionMap : ClassMap<Region>
    {
        public RegionMap()
        {
            Id(x => x.Id)
                .GeneratedBy.GuidComb();

            HasManyToMany(x => x.Products)
                .Access.BackingField()
                .Inverse()
                .AsSet();
        }
    }

    public class RetailerMap : ClassMap<Retailer>
    {
        public RetailerMap()
        {
            Id(x => x.Id)
                .GeneratedBy.GuidComb();

            HasManyToMany(x => x.Products)
                .Access.BackingField()
                .Inverse()
                .AsSet();
        }
    }

    public class Product
    {
        public virtual Guid Id { get; protected set; }

        public virtual Iesi.Collections.Generic.ISet<Retailer> Retailers { get; private set; }

        public virtual Iesi.Collections.Generic.ISet<Region> Regions { get; private set; }

        public Product()
        {
            Retailers = new Iesi.Collections.Generic.HashedSet<Retailer>();
            Regions = new Iesi.Collections.Generic.HashedSet<Region>();
        }
    }

    public class Retailer
    {
        public virtual Guid Id { get; protected set; }

        public virtual Iesi.Collections.Generic.ISet<Product> Products { get; set; }

        public Retailer()
        {
            Products = new Iesi.Collections.Generic.HashedSet<Product>();
        }
    }

    public class Region
    {
        public virtual Guid Id { get; protected set; }

        public virtual Iesi.Collections.Generic.ISet<Product> Products { get; set; }

        public Region()
        {
            Products = new Iesi.Collections.Generic.HashedSet<Product>();
        }
    }
}