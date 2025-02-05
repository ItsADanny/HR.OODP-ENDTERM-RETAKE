class NetworkRouter : Machine
{
    public readonly string Ports; // Change type to a 2D string array
    public const int MaxConnectionsPerPort = 2;
    
    public NetworkRouter(string name, int numPorts) : base(name)
    {

    }

    public void ConnectDevice(int portRow, int connectionColumn, string ipAddress)
    {
        
    }
}
