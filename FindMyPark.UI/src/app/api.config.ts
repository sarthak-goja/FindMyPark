import { Capacitor } from '@capacitor/core';

export const getApiBaseUrl = (): string => {
    if (Capacitor.isNativePlatform()) {
        return 'http://10.0.2.2:5175'; // Android Emulator loopback to Host
    }
    return 'http://10.0.2.2:5175'; // Web Browser
};
