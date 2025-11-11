export default interface Evento{
    id?: string;
    nome: string;
    descricao: string;
    // . Precisa ver qual o tipo de dado que tem que colocar nas datas, eu não lembro, no DB é DateTime
    dataInicio: string; 
    dataFim: string;
    // . 
    criadoEm?: string;
    ativo?: boolean;
}