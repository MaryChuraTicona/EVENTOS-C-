using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace Reniec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public async Task<PersonaReniec> ConsultarDni(string dni)
        {
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImZyYW5jb21wMDI4QGdtYWlsLmNvbSJ9.JiCSOMkmlg_tbxXM1Hv_wgdsLsq2hhKM6NZsKb7iHU4"; // <-- Cambiar
            string url = $"https://dniruc.apisperu.com/api/v1/dni/{dni}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;

                PersonaReniec persona = JsonConvert.DeserializeObject<PersonaReniec>(json);
                return persona;
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();

            if (dni.Length != 8)
            {
                MessageBox.Show("Ingrese un DNI válido.");
                return;
            }

            var persona = await ConsultarDni(dni);

            if (persona == null)
            {
                MessageBox.Show("DNI no encontrado.");
                return;
            }

            // Llenar tus textbox
            txtNombres.Text = persona.nombres;
            txtApePaterno.Text = persona.apellidoPaterno;
            txtApeMaterno.Text = persona.apellidoMaterno;
        }

        
    }
}
