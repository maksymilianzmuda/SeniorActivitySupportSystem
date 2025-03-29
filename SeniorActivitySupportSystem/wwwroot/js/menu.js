function toggleMenu() {
    const menu = document.getElementById("menu");
    const overlay = document.getElementById("overlay");
    const isOpen = menu.classList.toggle("open");

    if (isOpen) {
        overlay.classList.add("show");
    } else {
        overlay.classList.remove("show");
    }
}

document.addEventListener("keydown", function (event) {
    if (event.key === "Escape") {
        const menu = document.getElementById("menu");
        const overlay = document.getElementById("overlay");

        if (menu.classList.contains("open")) {
            menu.classList.remove("open");
            overlay.classList.remove("show");
        }
    }
});
