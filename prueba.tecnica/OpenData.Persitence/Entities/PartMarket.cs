namespace OpenData.Persistence.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table(name: "PartMarket", Schema = "dbo")]
public class PartMarket
{
    public PartMarket(string brpCode)
    {
        BrpCode = brpCode;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; private set; }

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string BrpCode { get; private set; } = string.Empty;

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string BrpName { get; private set; } = string.Empty;

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string Country { get; private set; } = string.Empty;

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string BusinessId { get; private set; } = string.Empty;

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string CodingScheme { get; private set; } = string.Empty;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime ValidityStart { get; private set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime ValidityEnd { get; private set; }

    public PartMarket WithBrpName(string brpName)
    {
        BrpName = brpName;
        return this;
    }

    public PartMarket WithCountry(string country)
    {
        Country = country;
        return this;
    }

    public PartMarket WithBusinesId(string businesId)
    {
        BusinessId = businesId;
        return this;
    }

    public PartMarket WithCodingScheme(string codingScheme)
    {
        CodingScheme = codingScheme;
        return this;
    }

    public PartMarket WithValidityStart(DateTime validityStart)
    {
        ValidityStart = validityStart;
        return this;
    }

    public PartMarket WithValidityEnd(DateTime validityEnd)
    {
        ValidityEnd = validityEnd;
        return this;
    }
}