

export function actualizarUI() {
    const token = !!localStorage.getItem("token");

    document.getElementById("btnLogin").classList.toggle("d-none", token);
    document.getElementById("btnRegister").classList.toggle("d-none", token);

    const btnLogout = document.getElementById("btnLogout");
    btnLogout.classList.toggle("d-none", !token);
    btnLogout.addEventListener("click", (e) => {
        e.preventDefault();
        localStorage.removeItem("token");
        window.location.href = "/";
    });
}
