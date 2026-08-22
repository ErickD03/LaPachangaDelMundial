window.descargarArchivo = function (nombreArchivo, contenido) {
    var elemento = document.createElement('a');
    elemento.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(contenido));
    elemento.setAttribute('download', nombreArchivo);
    document.body.appendChild(elemento);
    elemento.click();
    document.body.removeChild(elemento);
};