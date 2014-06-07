public class Pontuacao {
	//campo privado no qual armazenamos a instancia estatica da classe
	private static Pontuacao instancia;
	/// <summary>
	/// Obtem a instancia dessa classe
	/// </summary>
	/// <value>A instancia de Pontuacao.</value>
	public static Pontuacao Instancia{
		//Esse e um recurso do C# que nos permite acessar o "getInstancia()" como se
		// fosse uma variavel publica
		get {
			if(instancia == null){
				instancia = new Pontuacao();
			}
			return instancia;
		}
	}
	//Construtor privado da classe. E privado porque as outras classes chamarao
	// a classe Pontuacao atraves do Pontuacao.Instancia
	private Pontuacao(){
		placar = 0;
	}
	//Campo privado e metodo acessador do placar
	private int placar;
	public int Placar{
		get { return Placar; }
	}
	//Metodo para adiçao de pontos ao placar
	public void acrescentarPontos(int pontos){
		if(pontos > 0){
			placar += pontos;
		}
	}
	//Metodo para se resetar a pontuacao
	public void resetarPontuacao(){
		placar = 0;
	}
}
