export module Server {
    export enum NotaChamado {
        Excelente = 1,
        MuitoBom = 2,
        Bom = 3,
        Razoavel = 4,
        Ruim = 5,
        Péssimo = 6
    }

    export enum TipoChamado {
        Duvida = 1,
        Sugestao = 2,
        Reclamacao = 3
    }

    export enum SituacaoChamado {
        Novo = 1,
        Atendendo = 2,
        Concluido = 3
    }

    export enum UrgenciaTipo {
        Baixa = 1,
        Media = 2,
        Alta = 3
    }

    export enum PessoaTipo {
        Cliente = 1,
        Funcionario = 2
    }

    export interface Pessoa {
        id?: number;
        nome?: string;
        email?: string;
        cpf?: string;
        tipo?: PessoaTipo;
        usuario?: string;
        senha?: string;
    }

    export interface Chamado {
        id?: number;
        clienteId?: number;
        cliente?: Server.Pessoa;
        titulo: string;
        tipo: TipoChamado;
        mensagem: string;
        respondido: boolean;
        urgencia: UrgenciaTipo;
        dataChamadoAbertura?: Date;
        dataChamadoFechamento?: Date;
        respostaAtendente: string;
        respostaCliente: string;
        tipoDescricao?: string;
        urgenciaDescricao?: string;
        situacao: SituacaoChamado;
        badgeCor?: string;
        badgeDescricao?: string;
    }

    export interface Feedback {
        id?: number;
        chamadoId?: number;
        chamado?: Server.Chamado;
        resolvido: boolean;
        nota: NotaChamado;
        mensagem: string;
    }

    export interface Relatorio {
        quantidadeChamados: number;
        quantidadeTipoSugestao: number;
        quantidadeTipoDuvida: number;
        quantidadeTipoReclamacao: number;
        quantidadeUrgenciaAlta: number;
        quantidadeUrgenciaMedia: number;
        quantidadeUrgenciaBaixa: number;
        mediaTempoResposta: number;
        quantidadeChamadosRespondidos: number;
        maiorQuantidadeTipo: string;
        quantidadeNotaExcelente: number;
        quantidadeNotaMuitoBom: number;
        quantidadeNotaBom: number;
        quantidadeNotaRazoavel: number;
        quantidadeNotaRuim: number;
        quantidadeNotaPessimo: number;
    }
}