using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WpfApp18
{
    internal class MySqlTest
    {
        MySqlConnection connection;

        public MySqlTest()
        {
            MySqlConnectionStringBuilder configuration = new MySqlConnectionStringBuilder();
            configuration.Server = "192.168.200.13";
            configuration.UserID = "student";
            configuration.Password = "student";
            configuration.Database = "db1125restoran";
            configuration.CharacterSet = "utf8mb4";
            string connectionString = configuration.ToString();
            connection = new MySqlConnection(connectionString);
        }

        bool TryOpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex) 
            {
                Logger.WriteMessage(ex.Message); //можно создать систему логирования
                //System.Windows.MessageBox.Show(ex.Message); //или сразу показать сообщение
                return false; 
            }
            return true;
        }

        public List<Position> GetPositions()
        {
            List<Position> positions = new List<Position>();
            if (TryOpenConnection())
            {
                string query = "select * from tbl_position";
                using (MySqlCommand mc = new MySqlCommand(query, connection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Position position = new Position
                        {
                            ID = dr.GetInt32("id"),
                            Title = dr.GetString("title"),
                            Description = dr.GetString("description")
                        };
                        positions.Add(position);
                    }
                }
                connection.Close();
            }
            return positions;
        }

        public void InsertPosition(string title, string descrition)
        {
            if (TryOpenConnection())
            {
                string query = $"INSERT INTO tbl_position (title, description) VALUES (@title, @description);";
                List<MySqlParameter> mySqls = new();
                mySqls.Add(new MySqlParameter("title", title));
                mySqls.Add(new MySqlParameter("description", descrition));
                MySqlHelper.ExecuteNonQuery(connection, query, mySqls.ToArray());
                connection.Close();
            }
        }

        public void UpdatePosition(Position position)
        {
            if (TryOpenConnection())
            {
                string query = $"UPDATE tbl_position SET title = @title, description = @description WHERE id = {position.ID}";
                List<MySqlParameter> mySqls = new();
                mySqls.Add(new MySqlParameter("title", position.Title));
                mySqls.Add(new MySqlParameter("description", position.Description));
                MySqlHelper.ExecuteNonQuery(connection, query, mySqls.ToArray());
                connection.Close();
            }
        }

        public void DeletePosition(Position position)
        {
            if (TryOpenConnection())
            {
                string query = $"DELETE FROM tbl_position WHERE id = {position.ID}";
                MySqlHelper.ExecuteNonQuery(connection, query);
                connection.Close();
            }
        }
    }
}
