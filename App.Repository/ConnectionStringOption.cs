namespace App.Repository
{
    public class ConnectionStringOption
    {
        public const string key = "ConnectionStrings";
        public string SqlServer { get; set; } = default!;
    }
}
