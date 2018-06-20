cls
Add-Type -Path "C:\Program Files\Common Files\microsoft shared\Web Server Extensions\15\ISAPI\Microsoft.SharePoint.Client.dll"  
Add-Type -Path "C:\Program Files\Common Files\microsoft shared\Web Server Extensions\15\ISAPI\Microsoft.SharePoint.Client.Runtime.dll"  


$siteURL = "https://sptrainermanimekalai.sharepoint.com/sites/Sp1"  
$userId = "Manimekalai@SPTrainerManimekalai.onmicrosoft.com"  
$Secure_String_Pwd = ConvertTo-SecureString "Kkrish@47" -AsPlainText -Force
#$pwd = Read-Host -Prompt "Ratsubo365.." -AsSecureString
$pwd = $Secure_String_Pwd
$creds = New-Object Microsoft.SharePoint.Client.SharePointOnlineCredentials($userId, $pwd)  
$ctx = New-Object Microsoft.SharePoint.Client.ClientContext($siteURL)  
$ctx.credentials = $creds

try{  
    
    $lists = $ctx.Web.Lists     
    $list = $ctx.Web.Lists.GetByTitle("Issues")  
    $listItemInfo = New-Object Microsoft.SharePoint.Client.ListItemCreationInformation  
    $listItem = $list.AddItem($listItemInfo)  
    $listItem["Title"] = "Test Item"  
    $listItem.Update()      
    $ctx.Load($list)      
    $ctx.ExecuteQuery()  
    Write-Host "Item Added with ID - " $listItem.Id      
}  
catch{  
    write-host "$($_.Exception.Message)" -foregroundcolor red  
}