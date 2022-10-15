# Run command line (as Administrator) and enter:
# powershell -ExecutionPolicy ByPass -File viesapi-com-test.ps1

write-output "----- BEGIN VIES API TEST -----"

$viesapi = new-object -comObject 'VIESAPI.VIESAPIClient'
$viesapi | get-member

$viesapi.URL = 'https://viesapi.eu/api-test'
$viesapi.Id = 'test_id'
$viesapi.Key = 'test_key'

$nip = '7171642051'
$nip_eu = 'PL' + $nip

write-output ""
write-output "   Test data:"
write-output ""
write-output "NIP:    $nip"
write-output "NIP EU: $nip_eu"
write-output ""

write-output "----- BEGIN VIES DATA -----"

$vies = $viesapi.GetVIESData($nip_eu)
$vies

write-output "----- BEGIN ACCOUNT STATUS -----"

$status = $viesapi.GetAccountStatus()
$status

write-output "----- EOF -----"
