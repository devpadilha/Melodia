public class JogadorController
{
    JogadorModel model;

    public JogadorController()
    {
        model = new JogadorModel();
    }

   public Jogador get(int id)
    {        
        return model.get(id);
    }
    
    public Jogador save(Jogador jogador)
    {
        return model.save(jogador);
    }
}
