using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHello_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. 입력값 읽기
                double value = 1;
                string content = "test";
                DateTime now = DateTime.Now;
                string nowString = now.ToString("yyyy-MM-dd HH:mm:ss");

                tbHi.Text = DateTime.Now.ToString() + " 데이터가 DB으로 송신 됩니다.";
                //tbHi.Text = btnHello.Content.ToString() + "World";  //클릭시, TextBlock에 글자가 써짐

                // 2. PostgreSQL 연결 문자열
                string connString = "Host=localhost;Port=5432;Username=postgres ;Password=6302 ;Database=postgres";

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string query = "INSERT INTO history_test (\"Date\", \"Value\", \"Content\") VALUES (@Date, @Value, @Content)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("Date", now);
                        cmd.Parameters.AddWithValue("Value", value);
                        cmd.Parameters.AddWithValue("Content", content);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("데이터가 성공적으로 송신 되었습니다.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromDB();
        }

        private void LoadDataFromDB()
        {
            try
            {
                string connString = "Host=localhost;Port=5432;Username=postgres ;Password=6302 ;Database=postgres";

                DataPanel.Children.Clear();

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT \"Date\", \"Value\", \"Content\" FROM history_test ORDER BY \"Date\" DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime date = reader.GetDateTime(0);
                            double value = reader.GetDouble(1);
                            string content = reader.GetString(2);

                            TextBlock tb = new TextBlock
                            {
                                Text = $"{date:yyyy-MM-dd HH:mm:ss} | {value} | {content}",
                                FontSize = 16,
                                Margin = new Thickness(5)
                            };

                            DataPanel.Children.Add(tb);
                        }
                    }
                }

                tbHi.Text = "데이터 불러오기 완료.";
            }
            catch (Exception ex)
            {
                tbHi.Text = "불러오기 실패: " + ex.Message;
            }
        }
    }
}