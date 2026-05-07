# Domain Models

## Menu

```csharp
class Menu
{
    Menu Create();
    void AddDinner(Dinner dinner);
    void RemoveDinner(Dinner dinner);
    void UpdateSection(MenuSection section);
}
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "menuName": "Banchan",
  "description": "A default menu",
  "averageRating": 4.5,
  "sections": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "name": "Apetizers",
      "description": "Starters",
      "items": [
        {
          "id": "00000000-0000-0000-0000-000000000000",
          "name": "Bulgogi",
          "description": "Chicken, rice",
          "price": 5.99
        }
      ]
    }
  ],
  "createdDateTime": "2026-05-03T12:00:00.0000000Z",
  "updatedDateTIme": "2026-05-03T12:00:00.0000000Z",
  "hostId": "00000000-0000-0000-0000-000000000000",
  "dinnerIds": [
    "00000000-0000-0000-0000-000000000000"
  ],
  "menuReviewsIds": [
    "00000000-0000-0000-0000-000000000000"
  ]
}
```