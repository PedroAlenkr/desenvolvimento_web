
function ver() {
    var x = document.getElementById("senha");
    if (x.type === "password") {
      x.type = "text";
    } else {
      x.type = "password";
    }
}

function admin() {
  var x = document.getElementById("tipo");
  x.value= "Admin";
}