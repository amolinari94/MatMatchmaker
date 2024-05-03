
namespace DataAccessLibrary
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
        Task<T> ExecuteScalar<T>(string sql, object parameters = null);
        Task<T> LoadSingleRecord<T, U>(string sql, U parameters);
        Task<int> ExecuteAsync(string sql, object parameters = null);

        //Task<List<string>> getSchoolNames(string sql, U parameters);


    }
}