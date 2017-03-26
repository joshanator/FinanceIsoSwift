
<?php

session_start();
    $target=$_POST['Uploads'];
        if($target[strlen($target)-1]!='/')
                $target=$target+'/';
            $count=0;
            foreach ($_FILES['input']['name'] as $filename) 
            {
                $temp=$target;
                $tmp=$_FILES['input']['tmp_name'][$count];
                $count=$count + 1;
                $temp=$temp.basename($filename);
                move_uploaded_file($tmp,$temp);
                $temp='';
                $tmp='';
            }
    header("financeisoswift.azurewebsites.net/upload.php");

echo json_encode([0]};
?> 