namespace dot_api.Dtos;

public record class ItemDto(
    int userId,
    int Quantity,
    string ItemName,
    string ItemPower
)