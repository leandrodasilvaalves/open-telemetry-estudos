using System.Text.Json;
internal class Options 
{
    public string UrlClient { get; set; }  
    public string OtelUrl { get; set; }     
    private string _serviceName;
    public string ServiceName
    {
        get => $"Lele.Servico.{_serviceName}"; 
        set =>  _serviceName = value; 
    }
    
    public string ServiceVersion { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}