using MySql.Data.MySqlClient;
using System.Data;

public class BDD
{
    #region Attributs
    protected MySqlConnection? mySqlConnection;
    protected bool connected;

    private string host;
    private string port;
    private string username;
    private string password;
    private string database;
	#endregion
	public BDD()
	{
		this.connected = false;
        this.host = "2.11.7.149";
        this.port = "3306";
        this.database = "VeloMax";
        this.username = "projet_bdd";
        this.password = "Hugo13Lounes03Hugo!";
        this.mySqlConnection = null;

    }

    public BDD(string username,string password)
    {
        this.connected = false;
        this.host = "2.11.7.149";
        this.port = "3306";
        this.database = "VeloMax";
        this.username = username;
        this.password = password;
        this.mySqlConnection = null;
    }

    #region Parametres_Attributs
    public bool Connected
	{
		get { return this.connected; }
		set { this.connected = value; }
	}

    public MySqlConnection? MySqlConnection
    {
        get { return this.mySqlConnection; }
    }
    #endregion

    public void Connect()
    {
        if (!this.connected)
        {
            this.mySqlConnection = new MySqlConnection("SERVER=" + this.host + ";PORT=" + this.port + ";DATABASE=" + this.database + ";UID=" + this.username+ ";PWD=" + this.password+";");
            try
            {
                if (this.mySqlConnection.State == ConnectionState.Closed) this.mySqlConnection.Open();
                this.connected = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }

    public void Disconnect()
    {
        if (this.connected)
        {
            this.mySqlConnection?.Close();
        }
    }
}
