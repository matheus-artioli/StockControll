document.addEventListener('DOMContentLoaded', () => {
    loadProdutoStats();
    loadProdutosTable();
});

async function loadProdutoStats() {
    try {
        const response = await fetch('http://localhost:5049/api/produtos/stats');
        if (!response.ok) throw new Error('Erro ao buscar estatísticas dos produtos');
        
        const data = await response.json();

        document.getElementById('total-produtos').textContent = data.totalProdutos;
        document.getElementById('total-itens-estoque').textContent = data.totalItensEstoque;
        document.getElementById('valor-total-estoque').textContent = data.valorTotalEstoque.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
        document.getElementById('produtos-baixo-estoque').textContent = data.produtosBaixoEstoque;

    } catch (error) {
        console.error("Falha ao carregar produtos:", error);
    }
}

async function loadProdutosTable() {
    try {
        const response = await fetch('http://localhost:5049/api/dashboard/stats');
        if (!response.ok) throw new Error('Erro ao buscar a lista de produtos');

        const produtos = await response.json();
        const tbody = document.getElementById('produtos-tbody');
        tbody.innerHTML = '';

        if (produtos.length === 0) {
            tbody.innerHTML = '<tr><td colspan="5" style="text-align:center;">Nenhum produto cadastrado.</td></tr>';
            return;
        }

        produtos.forEach(produto => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${produto.codigo}</td>
                <td>${produto.nome}</td>
                <td>${produto.qtd_estoque}</td>
                <td>${produto.preco.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</td>
                <td>${produto.fornecedor ? produto.fornecedor.nome : 'N/A'}</td>
            `;
            tbody.appendChild(tr);
        });

    } catch (error) {
        console.error("Falha ao carregar a tabela de produtos:", error);
    }
}