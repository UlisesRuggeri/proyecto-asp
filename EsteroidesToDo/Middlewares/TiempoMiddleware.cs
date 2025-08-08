/*namespace EsteroidesToDo.Middlewares
{
    public class TiempoMiddleware
    {
        private readonly RequestDelegate _next;

        public TiempoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ruta = context.Request.Path;
            var inicio = DateTime.Now;

            Console.WriteLine($"[Middleware] Entrando a la ruta: {ruta}");

            await _next(context); // Sigue con el siguiente middleware o controlador

            var fin = DateTime.Now;
            var duracion = fin - inicio;

            Console.WriteLine($"[Middleware] Salió de la ruta: {ruta} en {duracion.TotalMilliseconds} ms");
        }
    }

}*/
