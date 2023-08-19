/*const alertStatus = (e) => {
    if ($("#gen").is(":checked") || $("#gen2").is(":checked")) {
      e = true
    } else {
      alert("Selecione uma das opções");
    }
  };
  
  // Attaching the click event on the button
  $(document).on("click", "#cadastrar", alertStatus)*/

  $("formCadastro").on("click", "#Cancelar", function(){
    $(this)[0].reset()
  })