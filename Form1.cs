

namespace wftest
{
    public partial class Form1 : Form
    {
        private String path;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen(object sender, EventArgs e)
        {
            textBox1.Clear();
            path = null;
            //openFileDialog1.InitialDirectory = "C:\\";//������ġ �������ϸ� ���� ��ġ ���� ���ϴ°� ����
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;//��ü���
                textBox1.Text = path.Split('\\')[path.Split('\\').Length - 1];//�����̸���
                richTextBox1.Text = File.ReadAllText(path);//�ؽ�Ʈ���� ���� ���

                // ���� ���� ��������
                FileInfo fileInfo = new FileInfo(path);

                // ���� �̸��� ũ�⸦ label1�� ǥ��
                label1.Text = $"���� �̸�: {fileInfo.Name}\n���� ũ��: {fileInfo.Length} ����Ʈ";
            }
        }

        private void btnEncode(object sender, EventArgs e)//��ȣȭ
        {
            string input = path.Split('\\')[path.Split('\\').Length - 1];
            input = input.Split('.')[0];
            string encodedfilename = input + "_encoded.txt";

            HuffmanCoding.CompressAndSaveToFile(path, encodedfilename);

            MessageBox.Show("��ȣȭ �Ϸ�!");//���� ũ�� ���
        }
        private void btnDecode(object sender, EventArgs e)//��ȣȭ
        {
            string input = path.Split('_')[0];
            string decodedfilename = input + "_decoded.txt";
            HuffmanCoding.DecompressAndSaveToFile(path, decodedfilename);

            MessageBox.Show("��ȣȭ �Ϸ�!");
            richTextBox1.Text = File.ReadAllText(decodedfilename);//�ؽ�Ʈ���� ���� ���
        }


    }
}