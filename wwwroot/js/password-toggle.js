(function () {
    document.querySelectorAll('[data-password-toggle]').forEach(function (btn) {
        var sel = btn.getAttribute('data-password-toggle');
        if (!sel) return;
        var input = document.querySelector(sel);
        if (!input) return;
        var icon = btn.querySelector('.js-password-icon');

        btn.addEventListener('click', function () {
            var mostrar = input.type === 'password';
            input.type = mostrar ? 'text' : 'password';
            btn.setAttribute('aria-label', mostrar ? 'Ocultar senha' : 'Mostrar senha');
            if (icon) {
                icon.textContent = mostrar ? '🙈' : '👁️';
            }
        });
    });
})();
