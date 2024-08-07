export interface LoginCommand {
    username: string,
    password: string,
    rememberMe: boolean
}

export interface LoginCommandResult {
    tokenAuth: string,
    refreshToken: string,
    isSucceed: boolean,
    message: string,
    userId: number
}