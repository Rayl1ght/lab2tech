using System.IO;
using System.Text.Json;

public class Klient
{
    public int Client_ID { get; set; }
    public string FIO_clienta { get; set; }
    public string Telefon_clienta { get; set; }
    public int ZakazID { get; set; }

    public Zakaz zakaz { get; set; }

    public Klient() { }

    public Klient(int Client_ID, string FIO_clienta, string Telefon_clienta, int ZakazID, Zakaz zakaz)
    {
        this.Client_ID = Client_ID;
        this.FIO_clienta = FIO_clienta;
        this.Telefon_clienta = Telefon_clienta;
        this.ZakazID = ZakazID;
        this.zakaz = zakaz;
    }
}

public class Zakaz
{
    public int Kodzakazauslugi { get; set; }

    public int IDyslugi { get; set; }

    public int IDclenta { get; set; }

    public int IDtipazakaza { get; set; }

    public int IDsotrudnika { get; set; }

    public Zakaz() { }

    public Zakaz(int Kodzakazauslugi, int IDyslugi, int IDclenta, int IDtipazakaza, int IDsotrudnika)
    {
        this.Kodzakazauslugi = Kodzakazauslugi;
        this.IDyslugi = IDyslugi;
        this.IDclenta = IDclenta;
        this.IDtipazakaza = IDtipazakaza;
        this.IDsotrudnika = IDsotrudnika;
    }
}



public class JsonHandler<T> where T : class
{
    private string fileName;
    JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };


    public JsonHandler() { }

    public JsonHandler(string fileName)
    {
        this.fileName = fileName;
    }


    public void SetFileName(string fileName)
    {
        this.fileName = fileName;
    }

    public void Write(List<T> list)
    {
        string jsonString = JsonSerializer.Serialize(list, options);

        if (new FileInfo(fileName).Length == 0)
        {
            File.WriteAllText(fileName, jsonString);
        }
        else
        {
            Console.WriteLine("Specified path file is not empty");
        }
    }

    public void Delete()
    {
        File.WriteAllText(fileName, string.Empty);
    }

    public void Rewrite(List<T> list)
    {
        string jsonString = JsonSerializer.Serialize(list, options);

        File.WriteAllText(fileName, jsonString);
    }

    public void Read(ref List<T> list)
    {
        if (File.Exists(fileName))
        {
            if (new FileInfo(fileName).Length != 0)
            {
                string jsonString = File.ReadAllText(fileName);
                list = JsonSerializer.Deserialize<List<T>>(jsonString);
            }
            else
            {
                Console.WriteLine("Specified path file is empty");
            }
        }
    }

    public void OutputJsonContents()
    {
        string jsonString = File.ReadAllText(fileName);

        Console.WriteLine(jsonString);
    }

    public void OutputSerializedList(List<T> list)
    {
        Console.WriteLine(JsonSerializer.Serialize(list, options));
    }
}



class Program
{
    static void Main(string[] args)
    {
        List<Klient> KlientList = new List<Klient>();

        JsonHandler<Klient> partsHandler = new JsonHandler<Klient>("klientFile.json");

        KlientList.Add(new Klient(1, "Sidorov", "8900", 1, new Zakaz(1, 1, 1, 1, 1)));
        KlientList.Add(new Klient(2, "Pilkin", "8901", 2, new Zakaz(2, 2, 2, 2, 2)));
        KlientList.Add(new Klient(3, "Visin", "8902", 3, new Zakaz(3, 3, 3, 3, 3)));
        KlientList.Add(new Klient(4, "Vzdorov", "8903", 4, new Zakaz(4, 4, 4, 4, 4)));
        partsHandler.Rewrite(KlientList);
        partsHandler.OutputJsonContents();
    }
}