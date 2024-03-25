using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace snglrtycrvtureofspce.User.Data.Entities;

public class ApplicationUserEntity : IdentityUser<Guid>
{
    [JsonPropertyName("refreshToken")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? RefreshToken { get; set; }
    
    [JsonPropertyName("refreshTokenExpiryTime")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    [JsonPropertyName("firstName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string FirstName { get; set; }
    
    [JsonPropertyName("lastName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string LastName { get; set; }
    
    [JsonPropertyName("lastName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? MiddleName  { get; set; }
    
    [JsonPropertyName("dateOfBirth")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime? DateOfBirth { get; set; }
    
    [JsonPropertyName("country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Country { get; set; }
    
    [JsonPropertyName("city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? City { get; set; }
    
    [JsonPropertyName("language")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Language { get; set; }

    [JsonPropertyName("userPhoto")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? UserPhoto { get; set; }
    
    [JsonPropertyName("facebookLink")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? FacebookLink { get; set; }
    
    [JsonPropertyName("instagramLink")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? InstagramLink { get; set; }
    
    [JsonPropertyName("twitterLink")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? TwitterLink { get; set; }
    
    [JsonPropertyName("vkLink")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? VkLink { get; set; }
    
    [JsonPropertyName("site")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Site { get; set; }

    [JsonPropertyName("agreement")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Agreement { get; set; }

    [JsonPropertyName("changePasswordNotification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? ChangePasswordNotification { get; set; }
    
    [JsonPropertyName("deliveryMethod")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? DeliveryMethod { get; set; }
    
    [JsonPropertyName("clientType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? ClientType { get; set; }
}