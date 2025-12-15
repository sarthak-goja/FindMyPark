$body = @{
  title = "Connaught Place Smart Parking"
  address = "Connaught Place, New Delhi"
  latitude = 28.6139
  longitude = 77.2090
  pricePerHour = 50
  isCovered = $true
  hasCCTV = $true
  hasEVCharger = $true
  isApproved = $true
  hostId = 1
} | ConvertTo-Json

try {
    $response = Invoke-RestMethod -Uri "http://localhost:5175/api/Listings" -Method Post -Body $body -ContentType "application/json"
    Write-Host "Seed Success: $($response.title)"
} catch {
    Write-Host "Seed Failed: $_"
}
