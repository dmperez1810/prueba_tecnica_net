namespace OpenData.Model.OpenData.Responses;

public class PartMarketDto
{
    public PartMarketDto(string brpCode)
    {
        BrpCode = brpCode;
        BrpName = string.Empty;
        Country = string.Empty;
        BusinessId = string.Empty;
        CodingScheme = string.Empty;
    }

    public PartMarketDto()
    {
        BrpCode = string.Empty;
        BrpName = string.Empty;
        Country = string.Empty;
        BusinessId = string.Empty;
        CodingScheme = string.Empty;
    }

    public string BrpCode { get; set; }
    public string BrpName { get; set; }
    public string Country { get; set; }
    public string BusinessId { get; set; }
    public string CodingScheme { get; set; }

    public DateTime ValidityStart { get; set; }
    public DateTime ValidityEnd { get; set; }

    public PartMarketDto WithBrpName(string brpName)
    {
        BrpName = brpName;
        return this;
    }

    public PartMarketDto WithCountry(string country)
    {
        Country = country;
        return this;
    }

    public PartMarketDto WithBusinesId(string businesId)
    {
        BusinessId = businesId;
        return this;
    }

    public PartMarketDto WithCodingScheme(string codingScheme)
    {
        CodingScheme = codingScheme;
        return this;
    }

    public PartMarketDto WithValidityStart(DateTime validityStart)
    {
        ValidityStart = validityStart;
        return this;
    }

    public PartMarketDto WithValidityEnd(DateTime validityEnd)
    {
        ValidityEnd = validityEnd;
        return this;
    }
}