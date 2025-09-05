document.addEventListener('DOMContentLoaded', () => {
    loadAllDashboardData();
});

function loadAllDashboardData() {
    loadStats();
    loadAlertaEstoque();
    loadTopVendas();
}

async function loadStats() {
    try {
        const response = await fetch('http://localhost:5049/api/dashboard/stats');
        if (!response.ok) {
            throw new Error('Erro ao buscar estatísticas dos cards.');
        }
        const data = await response.json();
        document.getElementById('total-produtos').textContent = data.totalProdutos;
        document.getElementById('total-fornecedores').textContent = data.totalFornecedores;
        document.getElementById('total-saidas').textContent = data.totalSaidas;
        document.getElementById('valor-gasto').textContent = data.valorGastoEmCompras.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    } catch (error) {
        console.error("Falha ao carregar KPIs:", error);
    }
}

async function loadAlertaEstoque() {
    try {
        const response = await fetch('http://localhost:5049/api/dashboard/alerta-estoque');
        if (!response.ok) throw new Error('Erro ao buscar alertas de estoque.');
        const produtosEmAlerta = await response.json();
        const tbody = document.getElementById('alerta-estoque-tbody');
        tbody.innerHTML = '';
        if (produtosEmAlerta.length === 0) {
            tbody.innerHTML = '<tr><td colspan="5" style="text-align:center;">Nenhum produto em alerta de estoque.</td></tr>';
            return;
        }
        produtosEmAlerta.forEach(produto => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>#${produto.id_produto}</td>
                <td>${produto.nome}</td>
                <td>${produto.qtd_estoque}</td>
                <td>${produto.estoqueMinimo}</td>
                <td><span style="color:red; font-weight:bold;">Baixo</span></td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        console.error("Falha ao carregar Alerta de Estoque:", error);
    }
}

async function loadTopVendas() {
    try {
        const response = await fetch('http://localhost:5049/api/dashboard/top-vendas');
        if (!response.ok) throw new Error('Erro ao buscar top vendas.');
        const topVendas = await response.json();
        const tbody = document.getElementById('top-vendas-tbody');
        tbody.innerHTML = '';
        if (topVendas.length === 0) {
            tbody.innerHTML = '<tr><td colspan="3" style="text-align:center;">Nenhum movimento de saída registrado.</td></tr>';
            return;
        }
        topVendas.forEach(produto => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>#${produto.idProduto}</td>
                <td>${produto.nomeProduto}</td>
                <td>${produto.totalVendido}</td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        console.error("Falha ao carregar Top Vendas:", error);
    }
}