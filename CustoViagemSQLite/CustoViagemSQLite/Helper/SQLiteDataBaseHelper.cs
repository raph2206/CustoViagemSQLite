using CustoViagemSQLite.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMercadin.Helper
{

    public class SQLiteDatabaseHelper
    {

        readonly SQLiteAsyncConnection _connection;

        public SQLiteDatabaseHelper(string path)
        {

            _connection = new SQLiteAsyncConnection(path);

            _connection.CreateTableAsync<Pedagio>().Wait();
        }

        public Task<int> Insert(Pedagio p)
        {
            return _connection.InsertAsync(p);
        }

        public Task<List<Pedagio>> Update(Pedagio p)
        {
            string sql = "UPDATE Pedagio SET Localizacao=?, Valor=? WHERE id= ? ";
            return _connection.QueryAsync<Pedagio>(sql, p.Localizacao, p.Valor , p.Id);
        }

        public Task<List<Pedagio>> GetAll()
        {
            return _connection.Table<Pedagio>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _connection.Table<Pedagio>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Pedagio>> Search(string q)
        {
            string sql = "SELECT * FROM Pedagio WHERE Descricao LIKE '%" + q + "%' ";

            return _connection.QueryAsync<Pedagio>(sql);
        }
    }
}