using MySql.Data.MySqlClient;
using REST_API.Models;
using System.Data;

namespace REST_API.Services
{
    public class DatabaseService
    {
        // Manupulation of Data
        private DBConnectionService DBConnectionService { get; }

        public DatabaseService(DBConnectionService service)
        {
            DBConnectionService = service;

            if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
            {
                DBConnectionService.OpenConnection();
            }
        }

        public void UpdateMember(Member member)
        {
            try
            {
                if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    DBConnectionService.OpenConnection();
                }

                Int32 recordCount = -1;

                string sql = String.Empty;

                if (member.Email != null)
                {
                    sql = $"UPDATE Member SET email = '{member.Email}',  WHERE memberid = {member.MemberID};";
                }

                MySqlCommand comm = new MySqlCommand(sql, DBConnectionService.GetConnection);

                Object result = comm.ExecuteNonQuery();

                recordCount = Convert.ToInt32(result);

                return;
            }
            catch (Exception ex)
            {
                return;
                //throw;
            }
        }

        public Int32 GetMemberCount()
        {
            try
            {
                if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    DBConnectionService.OpenConnection();
                }

                //DataSet ds = new DataSet();
                Int32 recordCount = -1;

                string sql = @"SELECT COUNT(""MemberID"") FROM {tableName};";
                sql = sql.Replace("{tableName}", "member");

                //dataAdapter.SelectCommand = new SqlCommand(maxIdxCommand, dbConnection.GetConnection);
                //dataAdapter.Fill(ds);

                MySqlCommand comm = new MySqlCommand(sql, DBConnectionService.GetConnection);

                Object result = comm.ExecuteScalar();

                recordCount = Convert.ToInt32(result);

                return recordCount;
            }
            catch (Exception)
            {
                return 1;
                //throw;
            }
        }

        public Member GetMemberByName(string byname)
        {
            try
            {
                if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    DBConnectionService.OpenConnection();
                }

                string sql = @"SELECT * FROM MEMBER WHERE name = '{byName}';";
                sql = sql.Replace("{byName}", byname);

                MySqlCommand comm = new MySqlCommand(sql, DBConnectionService.GetConnection);
                MySqlDataReader dataReader = comm.ExecuteReader();

                Member mmbr = new Member();

                dataReader.Read();
                mmbr.MemberID = Convert.ToInt32(dataReader[0]);
                mmbr.Name = dataReader[1].ToString();
                mmbr.Email = dataReader[2].ToString();
                mmbr.Password = dataReader[3].ToString();
                mmbr.PhoneNumber = dataReader[4].ToString();


                //string jsonObj = JsonConvert.SerializeObject(usr);

                return mmbr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Member GetMemberByEmail(string byemail)
        {
            try
            {
                if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    DBConnectionService.OpenConnection();
                }

                string sql = @"SELECT * FROM MEMBER WHERE email = '{byEmail}';";
                sql = sql.Replace("{byEmail}", byemail);

                MySqlCommand comm = new MySqlCommand(sql, DBConnectionService.GetConnection);
                MySqlDataReader dataReader = comm.ExecuteReader();

                Member mmbr = new Member();

                dataReader.Read();
                mmbr.MemberID = Convert.ToInt32(dataReader[0]);
                mmbr.Name = dataReader[1].ToString();
                mmbr.Email = dataReader[2].ToString();
                mmbr.Password = dataReader[3].ToString();
                mmbr.PhoneNumber = dataReader[4].ToString();

                //string jsonObj = JsonConvert.SerializeObject(usr);

                return mmbr;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Member GetMemberById(string byid)
        {
            try
            {
                if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    DBConnectionService.OpenConnection();
                }

                string sql = @"SELECT * FROM MEMBER WHERE memberid = '{byMemberID}';";
                sql = sql.Replace("{byMemberID}", byid);

                MySqlCommand comm = new MySqlCommand(sql, DBConnectionService.GetConnection);
                MySqlDataReader dataReader = comm.ExecuteReader();

                Member mmbr = new Member();

                dataReader.Read();
                mmbr.MemberID = Convert.ToInt32(dataReader[0]);
                mmbr.Name = dataReader[1].ToString();
                mmbr.Email = dataReader[2].ToString();
                mmbr.Password = dataReader[3].ToString();
                mmbr.PhoneNumber = dataReader[4].ToString();

                return mmbr;
            }
            catch (Exception)
            {

                throw;
            }
        }



        private Int32 MaxRecNo(string tableName)
        {
            try
            {
                if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    DBConnectionService.OpenConnection();
                }

                Int32 maxIdx = -1;

                string maxIdxCommand = @"SELECT MAX(MemberID) FROM {tableName};";
                maxIdxCommand = maxIdxCommand.Replace("{tableName}", tableName);

                MySqlCommand comm = new MySqlCommand(maxIdxCommand, DBConnectionService.GetConnection);

                Object Result = comm.ExecuteScalar();

                maxIdx = Convert.ToInt32(Result);

                return ++maxIdx;
            }
            catch (Exception)
            {
                return 1;
                //throw;
            }
        }

        public Member InsertNewMember(Member member)
        {
            try
            {
                if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    DBConnectionService.OpenConnection();
                }

                Int32 maxIdx = MaxRecNo("Member");

                string sql = @"INSERT INTO {tableName} (MemberID, Name, Email, Password, PhoneNumber) VALUES ({MemberID}, '{Name}', '{Email}', '{Password}', '{PhoneNumber}');";
                sql = sql.Replace("{tableName}", "Member");
                sql = sql.Replace("{MemberID}", maxIdx.ToString());
                sql = sql.Replace("{Name}", member.Name);
                sql = sql.Replace("{Email}", member.Email);
                sql = sql.Replace("{Password}", member.Password);
                sql = sql.Replace("{PhoneNumber}", member.PhoneNumber);


                member.MemberID = maxIdx;

                MySqlCommand comm = new MySqlCommand(sql, DBConnectionService.GetConnection);

                comm.ExecuteNonQuery();

                return member;
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
        }

        public List<Member> GetMemberList()
        {
            List<Member> memberList = new List<Member>();

            try
            {
                if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    DBConnectionService.OpenConnection();
                }

                string sql = @"SELECT * FROM {tableName}";
                sql = sql.Replace("{tableName}", "member");

                MySqlCommand comm = new MySqlCommand(sql, DBConnectionService.GetConnection);

                MySqlDataReader mysqlReader = comm.ExecuteReader();

                while (mysqlReader.Read())
                {
                    Member _member = new Member();
                    _member.MemberID = Convert.ToInt32(mysqlReader[0]);
                    _member.Name = mysqlReader[1].ToString();
                    _member.Email = mysqlReader[2].ToString();
                    _member.Password = mysqlReader[3].ToString();
                    _member.PhoneNumber = mysqlReader[4].ToString();


                    memberList.Add(_member);
                }

                mysqlReader.Close();

                //string obj = JsonConvert.SerializeObject(userList, Formatting.Indented);

                return memberList;
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        public void DeleteMember(int id)
        {
            try
            {
                if (DBConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    DBConnectionService.OpenConnection();
                }

                string sql = $"DELETE FROM Member WHERE MemberID = {id}";
                sql = sql.Replace("{id}", id.ToString());

                MySqlCommand comm = new MySqlCommand(sql, DBConnectionService.GetConnection);

                comm.ExecuteNonQuery();

                return;
            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }
    }
}
