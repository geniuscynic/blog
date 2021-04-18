using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DoCare.Extension.DataBase.Utility
{
//   public class TableDefind
//    {
//        public string ColumnName { get; set; }
//        public string DataTypeName { get; set; }

//        public int ColumnOrder { get; set; }

//        public int Length { get; set; }

//        public int IsNullable { get; set; }

//        public int IsIdentity { get; set; }
//    }
//    public class GenerateModel
//    {
//        private Dbclient client;
//        public GenerateModel(string connectionString, string provider)
//        {
//            client = new Dbclient(connectionString, provider);
//        }

//        public string MsSql = @"select c.name as ColumnName,
//c.colorder as ColumnOrder,
//c.xtype as DataType,
//typ.name as  DataTypeName,
//c.Length, c.isnullable as IsNullable,
//COLUMNPROPERTY (c.id, c.name,  'IsIdentity ') as IsIdentity
//from dbo.syscolumns c 
//inner join dbo.sysobjects t
//on c.id = t.id
//inner join dbo.systypes typ on typ.xtype = c.xtype
//where OBJECTPROPERTY(t.id, N'IsUserTable') = 1
//and t.name='{0}' order by c.colorder;";

//        public string ModelTemplate = "public class {}";


//        public async Task<IEnumerable<TableDefind>> GetTableDefine(string tableName)
//        {
//           return await client.GetConnection().QueryAsync<TableDefind>(string.Format(MsSql, tableName));
//        }

//        public async Task Generate(string tableName)
//        {
//            var results = await GetTableDefine(tableName);


//        }
    //}
}
