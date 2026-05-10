(function () {
    var cepInput = document.getElementById('Cep');
    if (!cepInput) return;

    var btnBuscar = document.getElementById('btnBuscarCep');
    var feedback = document.getElementById('viacep-feedback');

    function apenasDigitos(v) {
        return (v || '').replace(/\D/g, '');
    }

    function mostrarFeedback(texto, tipo) {
        if (!feedback) return;
        if (!texto) {
            feedback.textContent = '';
            feedback.hidden = true;
            feedback.className = 'small mt-1';
            return;
        }
        feedback.hidden = false;
        feedback.textContent = texto;
        if (tipo === 'erro') {
            feedback.className = 'small mt-1 text-danger';
        } else if (tipo === 'ok') {
            feedback.className = 'small mt-1 text-success';
        } else {
            feedback.className = 'small mt-1 text-muted';
        }
    }

    function preencherCampos(data) {
        var log = document.getElementById('Logradouro');
        var bai = document.getElementById('Bairro');
        var cid = document.getElementById('Cidade');
        var uf = document.getElementById('Uf');
        if (log) log.value = data.logradouro || '';
        if (bai) bai.value = data.bairro || '';
        if (cid) cid.value = data.localidade || '';
        if (uf) uf.value = (data.uf || '').toUpperCase();
    }

    async function buscarViaCep() {
        mostrarFeedback('', '');
        var cep = apenasDigitos(cepInput.value);
        if (cep.length !== 8) {
            mostrarFeedback('Informe um CEP com 8 dígitos.', 'erro');
            return;
        }
        mostrarFeedback('Consultando CEP...', 'info');
        try {
            var url = 'https://viacep.com.br/ws/' + cep + '/json/';
            var res = await fetch(url);
            if (!res.ok) throw new Error('HTTP ' + res.status);
            var data = await res.json();
            if (data.erro) {
                mostrarFeedback('CEP não encontrado.', 'erro');
                return;
            }
            preencherCampos(data);
            mostrarFeedback('Rua, bairro, cidade e estado preenchidos. Confira número e complemento.', 'ok');
        } catch (e) {
            mostrarFeedback('Não foi possível consultar o CEP. Verifique a conexão e tente novamente.', 'erro');
        }
    }

    cepInput.addEventListener('blur', function () {
        if (apenasDigitos(cepInput.value).length === 8) {
            buscarViaCep();
        }
    });

    if (btnBuscar) {
        btnBuscar.addEventListener('click', buscarViaCep);
    }
})();
