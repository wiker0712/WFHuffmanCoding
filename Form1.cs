

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
            //openFileDialog1.InitialDirectory = "C:\\";//시작위치 설정안하면 이전 위치 설정 안하는게 낫다
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;//전체경로
                textBox1.Text = path.Split('\\')[path.Split('\\').Length - 1];//파일이름만
                richTextBox1.Text = File.ReadAllText(path);//텍스트파일 내용 출력

                // 파일 정보 가져오기
                FileInfo fileInfo = new FileInfo(path);

                // 파일 이름과 크기를 label1에 표시
                label1.Text = $"파일 이름: {fileInfo.Name}\n파일 크기: {fileInfo.Length} 바이트";
            }
        }

        private void btnEncode(object sender, EventArgs e)//부호화
        {
            string input = path.Split('\\')[path.Split('\\').Length - 1];
            input = input.Split('.')[0];
            string encodedfilename = input + "_encoded.txt";

            HuffmanCoding.CompressAndSaveToFile(path, encodedfilename);

            MessageBox.Show("부호화 완료!");//파일 크기 출력
        }
        private void btnDecode(object sender, EventArgs e)//복호화
        {
            string input = path.Split('_')[0];
            string decodedfilename = input + "_decoded.txt";
            HuffmanCoding.DecompressAndSaveToFile(path, decodedfilename);

            MessageBox.Show("복호화 완료!");
            richTextBox1.Text = File.ReadAllText(decodedfilename);//텍스트파일 내용 출력
        }


    }
}