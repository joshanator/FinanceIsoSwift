
<?php
$uploads_dir = '/uploads';
foreach ($_FILES["input"]["error"] as $key => $error) {
    if ($error == UPLOAD_ERR_OK) {
        $tmp_name = $_FILES["input"]["tmp_name"][$key];
        // basename() may prevent filesystem traversal attacks;
        // further validation/sanitation of the filename may be appropriate
        $name = basename($_FILES["input"]["name"][$key]);
        move_uploaded_file($tmp_name, "$uploads_dir/$name");
    }
}

$response = file_get_contents('http://swiftapi.azurewebsites.net/api/validate/?xml=pain.001.001.08');

$response = json_decode($response);
console.log($response);


echo json_encode([0]};
?> 