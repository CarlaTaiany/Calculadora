using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Calculadora : Form
    {


        private bool entradaOperador = false;
        private bool expressaoVazia = true;
        public Calculadora()
        {
            InitializeComponent();

        }
        private void Numero_Click(object sender, EventArgs e)
        {
            //Pegar as infos que está chamando o evento:
            Button botaoClicado = (Button)sender;
            //Mostrar na tela o número que foi clicado, juntando com o número que já estava lá:
            txbTela.Text += botaoClicado.Text;
            entradaOperador = false; // A última entrada agora é um número
            expressaoVazia = false;  // A tela não está mais vazia
        }

        private void Operador_Click(object sender, EventArgs e)
        {

            // Só permite adicionar um operador se a última entrada não foi um operador,
            // e se a expressão não estiver vazia
            if (!entradaOperador && !expressaoVazia)
            {
                //Pegar as infos que está chamando o evento:
                Button botaoClicado = (Button)sender;
                //Mostrar na tela o número que foi clicado, juntando com o número que já estava lá:
                txbTela.Text += botaoClicado.Text;
                entradaOperador = true;
            }
            else if (expressaoVazia)
            {
                MessageBox.Show("Por favor, digite um número antes de um operador.", "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnIgual_Click(object sender, EventArgs e)
        {
            if (expressaoVazia)
            {
                MessageBox.Show("Não há valores para calcular!", "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string expressao = txbTela.Text;

                DataTable dt = new DataTable();
                try
                {
                    var v = dt.Compute(txbTela.Text.Replace("X", "*").Replace("x", "*").Replace("÷", "/"), "");
                    txbTela.Text = v.ToString();
                    // O resultado é como um número para a próxima operação
                    entradaOperador = false;
                    // A tela não está vazia após o cálculo
                    expressaoVazia = false;

                    if (txbTela.Text == "∞")
                    {
                        btnLimpar.PerformClick();
                        MessageBox.Show("Não é possível dividir por zero!", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                catch
                {

                    MessageBox.Show("Opa, algo deu ruim!");
                    expressaoVazia = true; // Reinicia o estado da expressão
                    entradaOperador = false;
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txbTela.Text = "";
            entradaOperador = false;
            expressaoVazia = true;

        }
    }
}



