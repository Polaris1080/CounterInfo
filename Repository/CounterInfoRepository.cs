using CounterInfo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterInfo.Repository
{
    public class CounterInfoRepository
    {
        SQLiteAsyncConnection? database;

        async Task Init()
        {
            if (database is not null) return;
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await database.CreateTableAsync<CounterInfomation>();
        }

        public async Task<List<CounterInfomation>> GetItemsAsync()
        {
            await Init();
            return await database!.Table<CounterInfomation>().ToListAsync();
        }

        //public async Task<List<CounterInfomation>> GetItemsNotCalledAsync()
        //{
        //    await Init();
        //    return await database!.Table<CounterInfomation>().Where(t => t.Called).ToListAsync();

        //    // SQL queries are also possible
        //    //return await database.QueryAsync<CounterInfomation>("SELECT * FROM [CounterInfomation] WHERE [Done] = 0");
        //}

        public async Task<CounterInfomation> GetItemAsync(int id)
        {
            await Init();
            return await database!.Table<CounterInfomation>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(CounterInfomation item)
        {
            await Init();
            if (item.ID != 0)
            {
                return await database!.UpdateAsync(item);
            }
            else
            {
                return await database!.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(CounterInfomation item)
        {
            await Init();
            return await database!.DeleteAsync(item);
        }
    }
}
