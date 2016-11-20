using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace OS_3k3n3n
{
    public partial class Form_main : Form
    {
        ToolHelp32 th;
        public Form_main()
        {
            InitializeComponent();
        }

        private void Form_main_Load(object sender, EventArgs e)
        {
            th = new ToolHelp32();
        }

        /// <summary>
        /// Отобращение всех процессов в диапазоне ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_showAll_Click(object sender, EventArgs e)
        {
            if (nud_to.Value < nud_from.Value)
            {
                MessageBox.Show("Неверно указан диапазон", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                nud_to.Focus();
                return;
            }
            tb_all.Clear();

            Thread AllThrd = new Thread(new ThreadStart(RunAll));
            AllThrd.Start();
        }

        /// <summary>
        /// Метод отображения всех процессов для потока
        /// </summary>
        void RunAll()
        {
            lock (this)
            {
                Invoke(new Action(()=>btn_showAll.Enabled = btn_execute.Enabled = false));
                foreach (ProcessEntry32 process in th.GetProcessesInIdRange((int)nud_from.Value, (int)nud_to.Value))
                {
                    tb_all.Invoke(new Action(()=>tb_all.AppendText(process.ToString() + "Количество модулей: " + th.CountModuleLength(process.th32ProcessID) + "\r\n"
                                      + "Общая память: " + th.CountModuleMemSize(process.th32ProcessID).ToString() + " байт\r\n\r\n")));
                }
                Invoke(new Action(() => btn_showAll.Enabled = btn_execute.Enabled = true));
            }
        }

        /// <summary>
        /// Выполнение условия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_execute_Click(object sender, EventArgs e)
        {
            if (nud_to.Value < nud_from.Value)
            {
                MessageBox.Show("Неверно указан диапазон", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                nud_to.Focus();
                return;
            }
            tb_res.Clear();
            tb_all.Clear();
            btn_showAll.PerformClick();

            Thread thrExec;
            if (rb_count.Checked)
                thrExec= new Thread(new ThreadStart(RunCount));
            else
                thrExec = new Thread(new ThreadStart(RunSize));
            thrExec.Start();
        }

        /// <summary>
        /// Метод для потока, считающего количества модулей
        /// </summary>
        void RunCount()
        {
            int maxLen = th.MaxModuleLength((int)nud_from.Value, (int)nud_to.Value);
            tb_res.Invoke(new Action(()=>tb_res.AppendText("Максимальная длина: " + maxLen.ToString() + "\r\n\r\n")));
            Invoke(new Action(() => btn_execute.Enabled = false));

            foreach (ProcessEntry32 process in th.GetProccessesMaxLenModsInRange((int)nud_from.Value, (int)nud_to.Value, maxLen))
            {
                tb_res.Invoke(new Action(() => tb_res.AppendText(process.ToString())));
                tb_res.Invoke(new Action(() => tb_res.AppendText("Количество модулей:: " + th.CountModuleLength(process.th32ProcessID).ToString()+"\r\n\r\n")));
            }

            Invoke(new Action(() => btn_execute.Enabled = true));
        }

        /// <summary>
        /// Метод для потока, считающего размер памяти модулей
        /// </summary>
        void RunSize()
        {
            long maxSize = th.MaxModuleMemSize((int)nud_from.Value, (int)nud_to.Value);
            tb_res.Invoke(new Action(() => tb_res.AppendText("Максимальный размер памяти: " + maxSize.ToString() + "\r\n\r\n")));
            Invoke(new Action(() => btn_execute.Enabled = false));

            foreach (ProcessEntry32 process in th.GetProccessesMaxMemSizeModsInRange((int)nud_from.Value, (int)nud_to.Value, maxSize))
            {
                tb_res.Invoke(new Action(() => tb_res.AppendText(process.ToString())));
                tb_res.Invoke(new Action(() => tb_res.AppendText("Память, занимаемая модулями: " + th.CountModuleMemSize(process.th32ProcessID).ToString() + "\r\n\r\n")));
            }

            Invoke(new Action(() => btn_execute.Enabled = true));
        }

        private void rb_count_CheckedChanged(object sender, EventArgs e)
        {
            tb_all.Clear();
            tb_res.Clear();
        }

        private void rb_size_CheckedChanged(object sender, EventArgs e)
        {
            tb_all.Clear();
            tb_res.Clear();
        }
    }
}
