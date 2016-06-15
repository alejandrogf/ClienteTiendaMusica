var idCategoria;
function filtrarPorCategoria(elem) {
    if (elem.Categoria == idCategoria)
        return elem;
}
$(function () {
    //Cuando cambia la dropdown list, se llama a la funcion
    $("#Categorias").change(function () {
        if ($(this).val() != "") {
            var firstChild = $(this).children().eq(0);
            if (firstChild.prop("value") == "") firstChild.remove();
        }
        idCategoria = this.value;
        CargarComponentes();
    });

});

function CargarComponentes() {
    $("#datos tbody tr").remove();
    var lc = JSON.parse(listaComp);

    var lcF = lc.filter(filtrarPorCategoria);

    var table = document.getElementById("datos").getElementsByTagName("tbody")[0];
    
    for (var i = 0; i < lcF.length; i++) {
        var row = table.insertRow(table.rows.length);

        var cell1 = row.insertCell(row.length);
        cell1.innerHTML = lcF[i].Nombre;

        var cell2 = row.insertCell(row.length);
        cell2.innerHTML = lcF[i].Descripcion;

        var cell3 = row.insertCell(row.length);
        cell3.innerHTML = lcF[i].PrecioBruto;

        var cell4 = row.insertCell(row.length);
        cell4.innerHTML = lcF[i].IVA;

        var cell5 = row.insertCell(row.length);
        cell5.innerHTML = lcF[i].PrecioNeto;

        var cell6 = row.insertCell(row.length);
        var btn = document.createElement("input");
        btn.type = "button";
        btn.id = lcF[i].ID;
        btn.className = "btn btn-info";
        btn.value = "Añadir al carrito";
        btn.dataset.componente = JSON.stringify( lcF[i]);
        btn.onclick = function () {
            alert("Has añadido correctamente un elemento");
            almacenarComponente(this);
        };
        
        cell6.appendChild(btn);
    }
}

function almacenarComponente(elem) {
    ////////////////////
    //Solo guardo los ID...
    //localStorage.removeItem("listID");
    
    
    if (sessionStorage.getItem("listID") != null) {
        var listaIDCompAnt = sessionStorage.getItem("listID");
        var nuevaListaID = listaIDCompAnt.concat("|", elem.id);
        sessionStorage.setItem("listID", nuevaListaID);
    } else {
        sessionStorage.setItem("listID", elem.id);
    }
}

function VerCarro() {
    var listaID = sessionStorage.getItem("listID").split("|");



    //$.ajax({
    //    url: url,
    //    type: "GET",
    //    dataType: "json",
    //    cache: false,
    //    data: listaID2,
    //    success: function (data) {
    //        alert("Carrito actualizado");
    //    },
    //    error: function (data) {
    //        alert("Error occured in Carrito");
    //    }
    //});
}

function mostrarCarrito() {
    $("#datos tbody tr").remove();
    var listaID = sessionStorage.getItem("listID").split("|");
    
    var table = document.getElementById("carrito").getElementsByTagName("tbody")[0];

    for (var i = 0; i < lc.length; i++) {
        var row = table.insertRow(table.rows.length);

        var cell1 = row.insertCell(row.length);
        cell1.innerHTML = lc[i].Nombre;

        var cell2 = row.insertCell(row.length);
        cell2.innerHTML = lc[i].Descripcion;

        var cell3 = row.insertCell(row.length);
        cell3.innerHTML = lc[i].PrecioBruto;

        var cell4 = row.insertCell(row.length);
        cell4.innerHTML = lc[i].IVA;

        var cell5 = row.insertCell(row.length);
        cell5.innerHTML = lc[i].PrecioNeto;

    }
}